using PayrollSystem.Domain.Core.ValueObjects.Bases;

namespace PayrollSystem.Domain.Core.ValueObjects.Management.WebServiceManagement;
public class WebServiceURL : BaseValueObject<WebServiceURL>
{
    public string Value { get; private set; }

    public static WebServiceURL FromString(string value) => new WebServiceURL(value);
    private WebServiceURL(string value)
    {
        Value = value;
    }
    private WebServiceURL()
    {

    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    #region Operator Overloading
    public static explicit operator string(WebServiceURL title) => title.Value;
    public static implicit operator WebServiceURL(string value) => new(value);
    #endregion

    #region Methods
    public override string ToString() => Value;

    #endregion
}

