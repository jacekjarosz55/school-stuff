using System.ComponentModel.DataAnnotations;
namespace Test_Bazy_Danych.Model
{

    


    public class Osoba
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Imie { get; set; }

        [Required]
        [MaxLength(35)]
        public string Nazwisko { get; set; }
        
        
        public int PlecId { get; set; }
        public Plec Plec { get; set; }


        public Osoba(string imie, string nazwisko, int plecId)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            PlecId = plecId;
        }
    }
}
