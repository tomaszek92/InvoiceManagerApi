using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManagerApi.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nip { get; set; }
        public string Address { get; set; }
        
        // Do przechowywania informacji o kraju
        // lepiej zrobić nową tabelę z listą wszystkich krajów i tutaj dać klucz obcy do tej tabeli.
        public string Country { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
