using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Application.DTOs
{
    public abstract class BaseFilter { }

    public class PaginatedResponse<T>
    {
        public int ItemsCount { get; set; }
        public List<T> Items { get; set; } = new List<T>();

        [JsonIgnore]
        public int? Page { get; set; }

        [JsonIgnore]
        public int? PageSize { get; set; }
    }

    public class PaginationRequest
    {
        private const int _maxLimit = 100;
        private const int _defaultLimit = 10;


        public int Limit { get; set; } = _defaultLimit;
        public int Offset { get; set; } = 1;

        public void Normalize()
        {
            if (Limit <= 0)
            {
                Limit = _defaultLimit;
            }
            else if (Limit > _maxLimit)
            {
                Limit = _maxLimit;
            }
            if (Offset < 0)
            {
                Offset = 0;
            }
        }
    }
}
