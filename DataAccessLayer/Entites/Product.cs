using System.ComponentModel.DataAnnotations;


namespace DataAccessLayer.Entites;
public class Product
{
    [Key]
    public Guid ProductID { get; set; }
    public string ProductName {  get; set; }
    public string Category { get; set; }
    public double? UnitPrice { get; set; }
    public int? QuantityInStock { get; set; }
}
