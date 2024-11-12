using System.ComponentModel.DataAnnotations;

namespace ApiNet.model;

public class TaskDTO // the validations must be done in backend
{
   
    public int id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name is too long")] // in time of execution
    public string name { get; set; }
    
    [Required(ErrorMessage = "Description is required")]
    [StringLength(200, ErrorMessage = "Description is too long")]
    public string description { get; set; }
    
    
    [Required(ErrorMessage = "Done is required")]
    public bool done { get; set; } 
    
    [DataType(DataType.Date, ErrorMessage = "Invalid date")]
    public DateTime date { get; set; } // ;P
    // ;P
    
    public TaskDTO() {} // serializations and desserializations
    

    public override string ToString()
    {
        return
            $"{nameof(id)}: {id}, {nameof(name)}: {name}, {nameof(date)}: {date}, {nameof(description)}: {description}, {nameof(done)}: {done}";
    }
}