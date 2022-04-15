﻿using System;

namespace BicycleCompany.PartModels.API.Models
{
    public class PartModel
    {
        public Guid Id { get; set; }
        public Guid PartId { get; set; }
        public string Name { get; set; }
        public int AvailableQuantity { get; set; }
        public decimal Price { get; set; }
        public Guid ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }
        public Part Part { get; set; }
    }
}
