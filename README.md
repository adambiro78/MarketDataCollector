Specification
Design and implement two classes, first a DataCollectionService which is a simple market data collector
class, a class called DeribitExchangeAdapter which implements the methods for communication
between the Deribit Exchange abstracting the exchange specific behaviors from the
DataCollectionService.
DataCollectionService’s only task is to collect a single point of BTC-PERPETUAL price and the
corresponding timestamp and save to a .json object with an arbitrary name to the local directory with
the structure based on the following example:
{
"SecuritySymbol": " BTC-PERPETUAL",
"AskPrice": 41232.5,
"BidPrice": 41232.8,
"Timestamp": 1550230036440
}

DeribitExchangeAdapter should implement the communication with Deribit based on their
documentation. The other responsibility of this class is hiding exchange specific methods and data
formats from the DataCollectionService class.

Other information:
o The documentation of Deribit can be found at https://docs.deribit.com , their API does not
require registration or any other form of authorization.
o Use the Test Deribit endpoints (test.deribit.com).
o Use the following Deribit API credential properties for authentication:
"grant_type": "client_credentials",
"client_id": "BNkS7vXd",
"client_secret": "SOzklmYh1dZrkaJGpWIgNDZ6nVnxmw6euk2T9I0DSaM"
o You can experiment with Deribit API here: https://test.deribit.com/api_console/
o Before accessing data via the appropriate endpoint, you may have to invoke the authorization
requrest first.
