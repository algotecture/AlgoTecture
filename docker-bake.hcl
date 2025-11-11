# ==========================================================
# Docker Bake configuration for AlgoTecture microservices
# Builds and publishes all service images to GHCR
# ==========================================================

variable "REGISTRY" {
  default = "ghcr.io"
}

variable "IMAGE_NS" {}

variable "BUILD_CONFIGURATION" {
  default = "Release"
}

group "default" {
  targets = [
    "identity",
    "user",
    "space",
    "reservation",
    "apigateway",
    "telegrambot"
  ]
}

# -------------------- Identity Service --------------------
target "identity" {
  context = "."
  dockerfile = "src/Services/Identity/AlgoTecture.Identity.Api/Dockerfile"
  tags = ["${REGISTRY}/${IMAGE_NS}/algotecture-identity:latest"]
  args = {
    BUILD_CONFIGURATION = "${BUILD_CONFIGURATION}"
  }
}

# -------------------- User Service --------------------
target "user" {
  context = "."
  dockerfile = "src/Services/User/AlgoTecture.User.Api/Dockerfile"
  tags = ["${REGISTRY}/${IMAGE_NS}/algotecture-user:latest"]
  args = {
    BUILD_CONFIGURATION = "${BUILD_CONFIGURATION}"
  }
}

# -------------------- Space Service --------------------
target "space" {
  context = "."
  dockerfile = "src/Services/Space/AlgoTecture.Space.Api/Dockerfile"
  tags = ["${REGISTRY}/${IMAGE_NS}/algotecture-space:latest"]
  args = {
    BUILD_CONFIGURATION = "${BUILD_CONFIGURATION}"
  }
}

# -------------------- Reservation Service --------------------
target "reservation" {
  context = "."
  dockerfile = "src/Services/Reservation/AlgoTecture.Reservation.Api/Dockerfile"
  tags = ["${REGISTRY}/${IMAGE_NS}/algotecture-reservation:latest"]
  args = {
    BUILD_CONFIGURATION = "${BUILD_CONFIGURATION}"
  }
}

# -------------------- API Gateway --------------------
target "apigateway" {
  context = "."
  dockerfile = "src/Services/ApiGateway/AlgoTecture.ApiGateway/Dockerfile"
  tags = ["${REGISTRY}/${IMAGE_NS}/algotecture-apigateway:latest"]
  args = {
    BUILD_CONFIGURATION = "${BUILD_CONFIGURATION}"
  }
}

# -------------------- Telegram Bot --------------------
target "telegrambot" {
  context = "."
  dockerfile = "src/TelegramBot/AlgoTecture.TelegramBot.Api/Dockerfile"
  tags = ["${REGISTRY}/${IMAGE_NS}/algotecture-telegrambot:latest"]
  args = {
    BUILD_CONFIGURATION = "${BUILD_CONFIGURATION}"
  }
}
