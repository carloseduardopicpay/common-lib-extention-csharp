namespace Domain.Entities
{
    public class DataParameter
    {
        public DataParameter(string nameParameter, object valueParameter)
        {
            NameParameter = string.Concat("@", nameParameter.Replace("@", ""));
            ValueParameter = valueParameter;
        }

        public string NameParameter { get; protected set; }

        public object ValueParameter { get; protected set; }
    }
}
