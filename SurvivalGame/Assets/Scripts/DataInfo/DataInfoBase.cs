using System.Collections;

public interface DataInfoBase
{
    EDataLoadResult Load();
    IEnumerator InitData();
}
