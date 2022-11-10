# Price calculator

## The task

The code in this project calculates the price of a product based on different types of users and product type

### Prerequisites

There are 2 types of products:
* Insurance
* Hardware

There are 2 types of users:
* Business (company)
* Consumer (normal)
* LargeCorporateUser (Corporate User)

### Requirements
The forumla to calculate the price is the following:

Product purchase price + product margin - discount

| Product type             | Margin   |
| ------------------------ | -------- |
| Insurance                | 25       |
| Hardware                 | 35       |

#### Discounts

If a campaign is active, 
* subtract 10 kronor in discount for users Business and Consumer
* As a business user you receive 5 kronor in discount extra
* LargeCorporateUser gets 10 % discount of the product


