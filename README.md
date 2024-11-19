## Hosted Service for Stock Trading

Implementation of a hosted service that consults Brapi and decides what trade should be made based on three values: current stock price, sell price, and buy price.
The last two values (and the desired stock) are informed via command-line arguments. The stock's current price is obtained via API request.

In this project, some important software concepts are implemented, such as single responsibility and dependency injection, two of the bare bones for a
reliable and extensible code. Besides that, object-oriented programming was also used in this solution.

Thinking about a future extension of this project, we could use Entity Framework to register and access the trade history, it could also be deployed as a microservice
for scalability purposes.
