variable "subscription_id" {
    type = string
}

variable "nstack_api_key" {
    type = string
}

variable "nstack_application_id" {
    type = string
}

variable "nstack_base_url" {
    type = string
    default = "https://nstack.io"
}

variable "app_service_name" {
    type = string
}

# Configure the Azure provider
terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
      version = ">= 2.26"
    }
  }
}

provider "azurerm" {
  subscription_id = var.subscription_id
  features {}
}

resource "azurerm_resource_group" "rg" {
  name     = "nstack-demo"
  location = "westeurope"
}

resource "azurerm_app_service_plan" "serviceplan" {
  name                = "nstack-appserviceplan"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  kind                = "Linux"
  reserved            = true

  sku {
    tier = "Free"
    size = "F1"
  }
}

resource "azurerm_app_service" "nstackdemo" {
  name                = var.app_service_name
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  app_service_plan_id = azurerm_app_service_plan.serviceplan.id
  https_only          = true

  app_settings = {
    "NStack_ApiKey" = var.nstack_api_key
    "NStack_ApplicationId" = var.nstack_application_id
    "NStack_BaseUrl" = var.nstack_base_url
  }
}