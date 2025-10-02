namespace Application.AuditsLog.DTOs;

public record AuditLogResponse
{
   

        public int Id { get; set; }

        public string Action { get; set; } = string.Empty;

        public string? TableName { get; set; }

        public int? RecordId { get; set; }

        public string? OldValues { get; set; }

        public string? NewValues { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;
  }



