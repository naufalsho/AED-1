namespace Core.Models
{
    public class PagingRequest
    {
        public int Page { get; set; }
        public int Length { get; set; }
        public List<PagingParameter> Parameters { get; set; }
        public PagingOrder Order { get; set; }
    }

    public class PagingParameter
    {
        public string Name { get; set; }
        public string SearchValue { get; set; }
    }

    public class PagingOrder
    {
        public int ParameterIndex { get; set; }
        public string Dir { get; set; }
    }

    public class PagingResponse<T>
    {
        public int RecordsFiltered { get { return _recordsFiltered; } }
        public int RecordsTotal { get { return _recordsTotal; } }
        public IEnumerable<T> Data { get { return _data; } }

        private int _recordsFiltered { get; set; }
        private int _recordsTotal { get; set; }
        private IEnumerable<T> _data { get; set; }

        public PagingResponse(int _recordsFiltered, int _recordsTotal, IEnumerable<T> _data)
        {
            this._recordsFiltered = _recordsFiltered;
            this._recordsTotal = _recordsTotal;
            this._data = _data;
        }

        public static PagingResponse<T> CreateResponse(int recordsFiltered, int recordsTotal, IEnumerable<T> data)
        {
            return new PagingResponse<T>(recordsFiltered, recordsTotal, data); ;
        }
    }
}
