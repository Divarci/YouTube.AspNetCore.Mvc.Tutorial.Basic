﻿namespace YouTube.AspNetCore.Tutorial.Basic.Models.Dto.InvoiceApiDto.ClientDto
{
    public class ClientUpdateDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Owner { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
