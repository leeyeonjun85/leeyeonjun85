using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore_MySQL.Models
{
    [Table("STUDENT")]
    public class Student
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("name")]
        [MaxLength(20)]
        [Required]
        public string Name { get; set; } = string.Empty;
    }
    


    //[Table("STUDENT")]
    //public class Student : INotifyPropertyChanging, INotifyPropertyChanged
    //{
    //    public event PropertyChangingEventHandler PropertyChanging;
    //    public event PropertyChangedEventHandler PropertyChanged;
    //    private int _id;
    //    private string _name;

    //    [Column("id")]
    //    [Key]
    //    public int Id
    //    {
    //        get => _id;
    //        set
    //        {
    //            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(nameof(Id)));
    //            _id = value;
    //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
    //        }
    //    }

    //    [Column("name")]
    //    [Required]
    //    [MaxLength(20)]
    //    public string Name
    //    {
    //        get => _name;
    //        set
    //        {
    //            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(nameof(Name)));
    //            _name = value;
    //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
    //        }
    //    }
    //}
}
