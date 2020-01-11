using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Authentication
{
    public class User : TableEntity
    {   
        public string Email => PartitionKey;
        public string Password => RowKey;
        public string Token { get; set; }
        public string Role { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string BlockedFor { get; set; }
        public DateTime? BlockedUntil { get; set; }
        public bool Active { get; set; }
        public DateTime? InvitedOn { get; set; }
        public string Avatar { get; set; }
    }
}
