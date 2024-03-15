using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Token_API.Models;

public class Token{
    
    [Key]
    [IgnoreDataMember]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    public string Name { get; set; }
    public string TotalSupply { get; set; }
    public string CirculatingSupply { get; set; }
    public DateTime Date { get; set; }
}