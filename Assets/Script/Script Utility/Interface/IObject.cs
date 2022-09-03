using System.Collections.Generic;

public interface IObject
{
    bool IsActive { get; }

    void Create(Dictionary<string, object> valueDic);
    void Hide();
    void Delete();
}
