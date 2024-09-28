using ChitChat.Domain.Entities;
using ChitChat.Domain.Entities.ChatEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.DataAccess.Configurations
{
    public class LoginHistoryConfiguration: IEntityTypeConfiguration<LoginHistory>
    {
        public void Configure(EntityTypeBuilder<LoginHistory> modelBuilder)
        {
            // Conversation Detail
            modelBuilder
            .HasKey(c => c.Id);


        }
    }
}
