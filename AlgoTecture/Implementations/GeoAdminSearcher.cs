using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AlgoTecture.Interfaces;
using AlgoTecture.Models.Dto;
using AlgoTecture.Models.GeoAdminModels;

namespace AlgoTecture.Implementations
{
    public class GeoAdminSearcher : IGeoAdminSearcher
    {
        public async Task<GeoAdminBuilding> GetBuildingModel(GeoAdminSearchBuildingModel geoAdminSearchBuildingModel)
        {
            if (geoAdminSearchBuildingModel == null) throw new ArgumentNullException(nameof(geoAdminSearchBuildingModel));

            var maddResponseTypeBuildings = await GetMaddResponseTypeBuildings(geoAdminSearchBuildingModel.FeatureId);

            GeoAdminBuilding geoAdminBuilding = new GeoAdminBuilding();
            if (maddResponseTypeBuildings != null && maddResponseTypeBuildings.Item != null &&
                ((maddResponseTypeBuildingList) maddResponseTypeBuildings.Item).buildingItem != null &&
                ((maddResponseTypeBuildingList) maddResponseTypeBuildings.Item).buildingItem.Length > 0)
            {
                geoAdminBuilding = BuildingFiller(maddResponseTypeBuildings);
            }

            return geoAdminBuilding;
        }

        private GeoAdminBuilding BuildingFiller(maddResponseType maddResponseTypeBuildings)
        {
            var geoAdminBuilding = new GeoAdminBuilding();

            var buildingItem = ((maddResponseTypeBuildingList) maddResponseTypeBuildings.Item).buildingItem[0];
            if (buildingItem.building != null)
            {
                var building = buildingItem.building;
                geoAdminBuilding.BuildingYear = 0;
                if (building.dateOfConstruction != null && building.dateOfConstruction.dateOfConstruction != null)
                {
                    var buildingDate = building.dateOfConstruction.dateOfConstruction;
                    geoAdminBuilding.BuildingYear = int.Parse(buildingDate.Split('-')[0]);
                }

                if (building.dateOfConstruction != null && building.dateOfConstruction.periodOfConstructionSpecified)
                {
                    var buildingPeriod = GwrCodeLookup.GetCode(building.dateOfConstruction.periodOfConstruction.ToString());

                    if (geoAdminBuilding.BuildingYear == 0)
                    {
                        geoAdminBuilding.BuildingYear = buildingPeriod switch
                        {
                            "Vor 1919" => 1919,
                            ">2015" => 2015,
                            _ => int.Parse(buildingPeriod.Split('-')[1])
                        };
                    }
                }

                geoAdminBuilding.BuildingCategory = GwrCodeLookup.GetCode(building.buildingCategory.ToString());

                if (building.buildingClass != null)
                {
                    geoAdminBuilding.BuildingClass = GwrCodeLookup.GetCode(building.buildingClass.ToString());
                }

                geoAdminBuilding.Levels = 0;
                if (building.numberOfFloors != null)
                {
                    geoAdminBuilding.Levels = Convert.ToInt32(building.numberOfFloors);
                }

                geoAdminBuilding.Area = 0;
                if (building.surfaceAreaOfBuilding != null)
                {
                    geoAdminBuilding.Area = Convert.ToInt32(building.surfaceAreaOfBuilding);
                }

                geoAdminBuilding.FloorArea = geoAdminBuilding.Levels * geoAdminBuilding.Area;
            }

            if (buildingItem.buildingEntranceList != null && buildingItem.buildingEntranceList.Length > 0)
            {
                var buildingEntranceItem = buildingItem.buildingEntranceList[0];
                if (buildingEntranceItem.dwellingList != null)
                {
                    geoAdminBuilding.Flats = buildingEntranceItem.dwellingList.Length;
                }

                if (buildingEntranceItem.buildingEntrance != null && buildingEntranceItem.buildingEntrance.locality != null)
                {
                    geoAdminBuilding.PlaceName = buildingEntranceItem.buildingEntrance.locality.placeName;
                }
            }

            if (buildingItem.municipality == null) return geoAdminBuilding;
            geoAdminBuilding.MunicipalityId = buildingItem.municipality.municipalityId;
            geoAdminBuilding.MunicipalityName = buildingItem.municipality.municipalityName;

            return geoAdminBuilding;
        }

        private async Task<maddResponseType> GetMaddResponseTypeBuildings(int featureId)
        {
            if (featureId <= 0) throw new ArgumentOutOfRangeException(nameof(featureId));

            const string baseUrl = "https://www.madd.bfs.admin.ch/eCH-0206";
            var date = DateTime.UtcNow.ToString("s") + "Z";
            var timestamp = ((int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
            const string baseContent =
                @"<?xml version=""1.0"" encoding=""UTF-8""?><eCH-0206:maddRequest xmlns:eCH-0058=""http://www.ech.ch/xmlns/eCH-0058/5"" xmlns:eCH-0206=""http://www.ech.ch/xmlns/eCH-0206/2"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.ech.ch/xmlns/eCH-0206/2 eCH-0206-2-0-draft.xsd""><eCH-0206:requestHeader><eCH-0206:messageId>{0}</eCH-0206:messageId><eCH-0206:businessReferenceId>{0}</eCH-0206:businessReferenceId><eCH-0206:requestingApplication><eCH-0058:manufacturer>BFH</eCH-0058:manufacturer><eCH-0058:product>AnaVis</eCH-0058:product><eCH-0058:productVersion>0.1</eCH-0058:productVersion></eCH-0206:requestingApplication><eCH-0206:requestDate>{1}</eCH-0206:requestDate></eCH-0206:requestHeader><eCH-0206:requestContext>building</eCH-0206:requestContext><eCH-0206:requestQuery><eCH-0206:EGID>{2}</eCH-0206:EGID></eCH-0206:requestQuery></eCH-0206:maddRequest>";

            var requestContent = string.Format(baseContent, timestamp, date, featureId);

            var request = WebRequest.Create(baseUrl);
            request.Method = WebRequestMethods.Http.Post;

            var postData = Encoding.ASCII.GetBytes(requestContent);

            request.ContentLength = postData.Length;

            await using (var stream = await request.GetRequestStreamAsync())
            {
                await stream.WriteAsync(postData.AsMemory(0, postData.Length));
            }

            var response = await request.GetResponseAsync();

            var data = response.GetResponseStream();

            var reader = new StreamReader(data ?? throw new InvalidOperationException(nameof(data)));

            var serializer = new XmlSerializer(typeof(maddResponseType));
            var buildings = (maddResponseType) serializer.Deserialize(reader);

            response.Close();

            return buildings;
        }
    }
}