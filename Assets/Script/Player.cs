public class Player {
    public int Id ;
    public int Position ; 
    public int MoneyInHand ;
    public int BankLoan ;
    public bool isActive;
    public string Name ;

    public Player(int Id, string Name){
        this.Id = Id;
        this.Name = Name;
        this.MoneyInHand = 0;
        this.isActive = true;
        this.BankLoan = 0;
        this.Position = 0;

    }
    public void deductMoney(int money)
    {
        MoneyInHand -= money;
    }

    public void allotMoney(int money){
        MoneyInHand += money;
    }

    public int getPosition()
    {
        return Position;
    }

    public int getId()
    {
        return Id;
    }
    public int getMoneyInHand()
    {
        return MoneyInHand;
    }
    public void setPosition(int Position)
    {
        this.Position = Position;
    }

    public bool checkMoneyAvailability(int money)
    {
        if (MoneyInHand < money)
            return false;
        else
            return true;
    }

    public void deactivatePlayer()
    {
        isActive = false;
    }

}
