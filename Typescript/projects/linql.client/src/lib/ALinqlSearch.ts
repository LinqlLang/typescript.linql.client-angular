export abstract class ALinqlSearch<T>
{

    constructor(public Type: (string | (new () => T)), public ArgumentContext: {} | undefined = {}, Context: ALinqlContext)
    {

    }
}

export abstract class ALinqlContext
{
    constructor(public LinqlSearchType: new (Type: (string | (new () => any)), ArgumentContext: {} | undefined, Context: ALinqlContext) => ALinqlSearch<any>)
    {

    }

    protected GetEndpoint(Search: ALinqlSearch<any>)
    {
        let endPoint: string;
        if (typeof Search.Type === "string")
        {
            endPoint = Search.Type;
        }
        else
        {
            endPoint = Search.Type.name;
        }
        return `linql/${ endPoint }`
    }

    public Set<T>(Type: new () => T, ArgumentContext: {} | undefined): ALinqlSearch<T>
    {
        return new this.LinqlSearchType(Type, ArgumentContext, this);
    }
}