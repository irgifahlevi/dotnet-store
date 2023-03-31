public class EventProduk
{
    public event EventHandler<string> Proses;

    public void TriggerEvent(string pesan)
    {
        OnProses(pesan);
    }

    protected virtual void OnProses(string pesan)
    {
        Proses?.Invoke(this, pesan);
    } 
}