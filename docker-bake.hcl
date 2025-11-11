variable "REGISTRY" { default = "ghcr.io" }
variable "IMAGE_NS" {}
variable "GIT_SHA" {}

function "tags" {
  params = [image]
  result = [
    "${REGISTRY}/${IMAGE_NS}/${image}:latest",
    "${REGISTRY}/${IMAGE_NS}/${image}:${GIT_SHA}",
  ]
}

group "default" {
  targets = ["identity","user","space","reservation","apigateway","telegrambot"]
}

target "identity" {
  tags = tags("algotecture-identity")
  context = "."
  dockerfile = "src/Services/Identity/AlgoTecture.Identity.Api/Dockerfile"
}

target "user" {
  tags = tags("algotecture-user")
  context = "."
  dockerfile = "src/Services/User/AlgoTecture.User.Api/Dockerfile"
}

target "space" {
  tags = tags("algotecture-space")
  context = "."
  dockerfile = "src/Services/Space/AlgoTecture.Space.Api/Dockerfile"
}

target "reservation" {
  tags = tags("algotecture-reservation")
  context = "."
  dockerfile = "src/Services/Reservation/AlgoTecture.Reservation.Api/Dockerfile"
}

target "apigateway" {
  tags = tags("algotecture-apigateway")
  context = "."
  dockerfile = "src/Services/ApiGateway/AlgoTecture.ApiGateway/Dockerfile"
}

target "telegrambot" {
  tags = tags("algotecture-telegrambot")
  context = "."
  dockerfile = "src/TelegramBot/AlgoTecture.TelegramBot.Api/Dockerfile"
}
