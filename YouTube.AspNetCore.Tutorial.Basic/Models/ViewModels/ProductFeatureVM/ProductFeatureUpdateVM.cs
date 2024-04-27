﻿using YouTube.AspNetCore.Tutorial.Basic.Models.Entity;

namespace YouTube.AspNetCore.Tutorial.Basic.Models.ViewModels.ProductFeatureVM
{
    public class ProductFeatureUpdateVM
    {
        public int Id { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string Colour { get; set; }
        public int ProductId { get; set; }
    }
}
