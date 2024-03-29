﻿namespace Android.Kotlin.Auth.API.Models;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Color { get; set; }
    public int Stock { get; set; }
    public string? PhotoPath { get; set; }
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }
    
}