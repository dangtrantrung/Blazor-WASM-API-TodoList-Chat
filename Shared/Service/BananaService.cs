namespace BlazingChat.Shared
{
public class BananaService:IBananaService
{
    public event Action Onchanged;
    public int Bananas {get;set;} =1000;
   public  void EatBananas (int amount)
   {
       Bananas-=amount;
       Bananachanged();
   }
   public  void AddBananas (int amount)
   {
       Bananas+=amount;
       Bananachanged();
   }
   void Bananachanged()=>Onchanged.Invoke();
}
}