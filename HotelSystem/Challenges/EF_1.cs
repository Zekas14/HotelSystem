/*
 Scenario: Optimizing Entity Framework Queries for Performance
Situation:
You are working on a backend application for an e-commerce platform. The application uses 
Entity Framework to interact with a SQL Server database. A particular feature displays a dashboard 
for administrators that shows a summary of orders, including total revenue, total orders, 
and the number of distinct customers for a given date range.
The existing implementation retrieves data from the database using Entity Framework, 
but administrators are complaining that the dashboard is slow to load when the date range 
covers a large period (e.g., several months).
Here is the current code for the query:
*/

/*
public async Task<OrderSummary> GetOrderSummaryAsync(DateTime startDate, DateTime endDate)
{
    using (var context = new ECommerceDbContext())
    {
        var orders = await context.Orders
            .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
            .ToListAsync();

        var totalRevenue = orders.Sum(o => o.TotalAmount);
        var totalOrders = orders.Count;
        var distinctCustomers = orders.Select(o => o.CustomerId).Distinct().Count();

        return new OrderSummary
        {
            TotalRevenue = totalRevenue,
            TotalOrders = totalOrders,
            DistinctCustomers = distinctCustomers
        };
    }
}
*/

/**** Solution ***/

/*
public async Task<OrderSummary> GetOrderSummaryAsync(DateTime startDate, DateTime endDate)
{
    using (var context = new ECommerceDbContext())
    {
        var summary = await context.Orders
            .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
            .GroupBy(o => 1)
            .Select(g => new OrderSummary
            {
                TotalRevenue = g.Sum(o => o.TotalAmount),
                TotalOrders = g.Count(),
                DistinctCustomers = g.Select(o => o.CustomerId).Distinct().Count()
            })
            .FirstOrDefaultAsync();

        return summary ?? new OrderSummary();
    }
}
*/
