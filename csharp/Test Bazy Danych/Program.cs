using System;
using System.Collections.Generic;
using System.Linq;
using Test_Bazy_Danych.Context;
using Test_Bazy_Danych.Model;
namespace Test_Bazy_Danych
{
    class Program
    {
        static void Main()
        {
            
        }

        private static void RemoveOsobaById(int id)
        {
            SqlContext context = new SqlContext();
            Osoba osoba = context.Osoby.FirstOrDefault(os => os.Id == id);
            context.SaveChanges();
            context.Dispose();
        }

        private static void RemoveOsoba(Osoba osoba)
        {
            SqlContext context = new SqlContext();
            context.Osoby.Remove(osoba);
            context.SaveChanges();
            context.Dispose();
        }

        private static void UpdateOsoba(int id)
        {
            SqlContext context = new SqlContext();
            Osoba osoba = context.Osoby.FirstOrDefault<Osoba>(os => os.Id == id);

            if (osoba != null)
            {
                osoba.Imie = "Karol";
                context.SaveChanges();
            }

            context.Dispose();
        }

        private static void ReadOsoba(int id)
        {
            SqlContext sqlContext = new SqlContext();

            Osoba osoba = sqlContext.Osoby.FirstOrDefault((Osoba os) => os.Id == id);
            Console.WriteLine($"Osoba {osoba.Id}: {osoba.Imie} {osoba.Nazwisko} - {osoba.Plec}");
            sqlContext.Dispose();
        }

        private static void CreateListOsoba(List<Osoba> listaOsob)
        {
            SqlContext sqlContext = new SqlContext();
            sqlContext.Osoby.AddRange(listaOsob);
            sqlContext.SaveChanges();
            sqlContext.Dispose();
        }

        private static Osoba GetOsobaById(int id)
        {
            SqlContext context = new SqlContext();
            Osoba osoba = context.Osoby.FirstOrDefault(os => os.Id == id);
            context.SaveChanges();
            context.Dispose();
            return osoba;
        }

        private static void CreateOsoba(Osoba osoba)
        {
            SqlContext sqlContext = new SqlContext();
            sqlContext.Osoby.Add(osoba);
            sqlContext.SaveChanges();
            sqlContext.Dispose();
        }
    }
}
