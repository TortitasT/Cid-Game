using System.Collections;
using System.Collections.Generic;

public class Inventory
{
    public List<Item> items;
    public Equipped equipped;

    public class Equipped {
        public Item head;
        public Item body;
        public Item legs;
        public Item feet;
    }
}
