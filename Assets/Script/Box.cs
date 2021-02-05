using System;

public class Box {
    public bool Available;
    public bool Ticket;
    public bool LoanTaken;
    public int Id;
    public int BankLoan;
    public int Cost;
    public int Rent;
    public int BankLoanRoundRemaining;
    public int PlayerId;
    public string Name;

    public static int INCOME_TAX_TILE = 21;
    public static int START_TILE = 0;
    public static int PRISON_TILE = 7;
    public static int HOTEL_TILE = 14;
    public static int BANK_LOAN_ROUNDS_LIMIT = 3;

    public Box(int Id, int Cost, int Rent,string Name){
        this.Id = Id;
        this.Cost = Cost;
        this.Rent = Rent;
        this.Name = Name;
        this.Available = true;
        this.PlayerId = -1;
        this.LoanTaken = false;
        this.BankLoan =(int) Math.Ceiling(Cost * 0.8);
        this.BankLoanRoundRemaining = -1;

        if (Id == INCOME_TAX_TILE || Id == START_TILE || Id == PRISON_TILE || Id == HOTEL_TILE)
            Ticket = false;
        else
            Ticket = true;

        
    }
    public void resetCity()
    {
        this.Available = true;
        this.PlayerId = -1;
        this.LoanTaken = false;
        this.BankLoanRoundRemaining = -1;
    }
    public int bankLoanAmmount()
    {
        return BankLoan;
    }
    public int getPlayerId()
    {
        return PlayerId;
    }
    public int getCost()
    {
        return Cost;
    }
    public bool getIsAvailable()
    {
        return Available;
    }
    public void lostToBank()
    {
        LoanTaken = false;
        Available = true;
        PlayerId = -1;
        BankLoanRoundRemaining = -1;

    }
    public void takeLoan()
    {
        LoanTaken = true;
        BankLoanRoundRemaining = BANK_LOAN_ROUNDS_LIMIT;

    }

    public void returnLoan()
    {
        LoanTaken = false;
        BankLoanRoundRemaining = -1;

    }

    public void reduceBankLoanRound()
    {
        BankLoanRoundRemaining--;
    }
    public bool isLimitExceed()
    {
        if (BankLoanRoundRemaining == 0)
            return true;
        else
            return false;
    }
    public void buyCity(int PlayerId)
    {
        Available = false;
        this.PlayerId = PlayerId;
    }
    public bool isLoanTaken()
    {
        return LoanTaken;
    }
    public int getOwner()
    {
        return PlayerId;
    }
    public int getRent()
    {
        return Rent;
    }
}
