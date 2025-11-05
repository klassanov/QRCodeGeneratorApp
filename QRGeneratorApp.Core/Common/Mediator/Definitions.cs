namespace QRGeneratorApp.Core.Common.Mediator
{
    public interface IQuery<TResult>;

    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(TQuery query);
    }
}
