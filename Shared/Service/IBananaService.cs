namespace BlazingChat.Shared
{

public interface IBananaService
{
    public event Action Onchanged;
    int Bananas {get;set;}
    void EatBananas (int amount);
    void AddBananas (int amount);
}
}