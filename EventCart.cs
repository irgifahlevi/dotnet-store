public class EventCart
{
    public event EventHandler<string> ProsesCart;

    public void TriggerCartEvent(string pesan)
    {
        OnProsesCart(pesan);
    }

    protected virtual void OnProsesCart(string pesan)
    {
        ProsesCart?.Invoke(this, pesan);
    } 
}