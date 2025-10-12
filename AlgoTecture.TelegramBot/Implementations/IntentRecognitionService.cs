using System.Text;
using System.Text.Json;
using AlgoTecture.TelegramBot.Implementations;
using AlgoTecture.TelegramBot.Models;
using Microsoft.Extensions.Options;

public class IntentRecognitionService
{
    private readonly DeepSeekService _deepSeekService;
    private readonly ILogger<IntentRecognitionService> _logger;

    public IntentRecognitionService(DeepSeekService deepSeekService, ILogger<IntentRecognitionService> logger)
    {
        _deepSeekService = deepSeekService;
        _logger = logger;
    }

    public async Task<BookingIntent> RecognizeIntentAsync(string userMessage)
    {
        try
        {
            var prompt = CreateIntentRecognitionPrompt(userMessage);
            var response = await _deepSeekService.GetResponseAsync(prompt);
            
            _logger.LogInformation("AI Response: {Response}", response);
            
            return ParseIntentResponse(response, userMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error recognizing intent for message: {Message}", userMessage);
            return CreateFallbackIntent(userMessage);
        }
    }

    private string CreateIntentRecognitionPrompt(string userMessage)
    {
        var today = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
        var tomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss");
        
        return $$"""
            Analyze the user's message in the PARKING SPACE RENTAL system.
            Determine the intent and extract parameters from the text.

            POSSIBLE ACTIONS:
            - create_booking - rent a parking space (keywords: "rent", "book", "reserve", "need parking", "parking spot")
            - cancel_booking - cancel rental (keywords: "cancel", "cancellation", "don't need", "won't make it")
            - check_availability - check availability (keywords: "available", "free spots", "any spaces", "check")
            - extend_booking - extend rental (keywords: "extend", "longer", "more time")
            - get_price - get price (keywords: "how much", "price", "cost")

            PARAMETERS TO EXTRACT:
            - address: parking address (look for street names, cities, districts, parking lots)
            - start_datetime: rental start date and time (format: yyyy-MM-ddTHH:mm:ss)
            - end_datetime: rental end date and time (format: yyyy-MM-ddTHH:mm:ss)
            - car_number: vehicle license plate (format: letters-numbers, e.g., "ABC123")
            - parking_type: space type ("covered", "open", "underground", "street")
            - customer_name: customer name
            - customer_phone: customer phone
            - booking_id: booking number (for cancellation/extension)

            IMPORTANT RULES:
            1. Extract date and time together in one datetime field
            2. Use ISO 8601 format: yyyy-MM-ddTHH:mm:ss
            3. If time is not specified, use 12:00:00 as default
            4. License plate should be in format "ABC123" (English letters)
            5. Phone should be in format "+1234567890"
            6. If action is not recognized - isValid = false
            7. For parking, ADDRESS and LICENSE PLATE are crucial

            Response format ONLY JSON:
            {
                "action": "action_name",
                "parameters": {
                    "address": "123 Main St, Parking Lot B",
                    "start_datetime": "{{today}}",
                    "end_datetime": "{{tomorrow}}", 
                    "car_number": "ABC123",
                    "parking_type": "covered",
                    "customer_name": "John",
                    "customer_phone": "+1234567890",
                    "booking_id": "PARK-001"
                },
                "isValid": true
            }

            Conversion examples:
            - "tomorrow at 10 AM" → "{{DateTime.Now.AddDays(1).Date.AddHours(10).ToString("yyyy-MM-ddTHH:mm:ss")}}"
            - "from Monday 14:00 to Friday 18:00" → calculate full datetimes
            - "next week from 9:00 to 17:00" → calculate dates and times

            User message: {{userMessage}}
            """;
    }

    private BookingIntent ParseIntentResponse(string jsonResponse, string originalMessage)
    {
        try
        {
            var cleanJson = ExtractJsonFromResponse(jsonResponse);
            
            var intent = JsonSerializer.Deserialize<BookingIntent>(cleanJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (intent != null)
            {
                intent.OriginalMessage = originalMessage;
                intent.IsValid = ValidateIntent(intent);
                return intent;
            }

            return CreateFallbackIntent(originalMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error parsing AI response: {Response}", jsonResponse);
            return CreateFallbackIntent(originalMessage);
        }
    }

    private string ExtractJsonFromResponse(string response)
    {
        var startIndex = response.IndexOf('{');
        var endIndex = response.LastIndexOf('}') + 1;
        
        if (startIndex >= 0 && endIndex > startIndex)
        {
            return response.Substring(startIndex, endIndex - startIndex);
        }
        
        return response;
    }

    private bool ValidateIntent(BookingIntent intent)
    {
        return intent.Action switch
        {
            "create_booking" => ValidateCreateBooking(intent),
            "cancel_booking" => ValidateCancelBooking(intent),
            "check_availability" => ValidateCheckAvailability(intent),
            "extend_booking" => ValidateExtendBooking(intent),
            "get_price" => ValidateGetPrice(intent),
            _ => false
        };
    }

    private bool ValidateCreateBooking(BookingIntent intent)
    {
        return intent.Parameters.ContainsKey("start_datetime") && 
               intent.Parameters.ContainsKey("end_datetime") &&
               intent.Parameters.ContainsKey("address") &&
               intent.Parameters.ContainsKey("car_number");
    }

    private bool ValidateCancelBooking(BookingIntent intent)
    {
        return intent.Parameters.ContainsKey("booking_id") ||
               (intent.Parameters.ContainsKey("address") && 
                intent.Parameters.ContainsKey("start_datetime") && 
                intent.Parameters.ContainsKey("car_number"));
    }

    private bool ValidateCheckAvailability(BookingIntent intent)
    {
        return intent.Parameters.ContainsKey("start_datetime") && 
               intent.Parameters.ContainsKey("end_datetime") &&
               intent.Parameters.ContainsKey("address");
    }

    private bool ValidateExtendBooking(BookingIntent intent)
    {
        return intent.Parameters.ContainsKey("booking_id") && 
               intent.Parameters.ContainsKey("end_datetime");
    }

    private bool ValidateGetPrice(BookingIntent intent)
    {
        return intent.Parameters.ContainsKey("start_datetime") && 
               intent.Parameters.ContainsKey("end_datetime") &&
               intent.Parameters.ContainsKey("address");
    }

    private BookingIntent CreateFallbackIntent(string originalMessage)
    {
        return new BookingIntent 
        { 
            Action = "unknown",
            OriginalMessage = originalMessage,
            IsValid = false
        };
    }
}