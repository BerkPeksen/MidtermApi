# RabbitMQ Queueing Service for University Tuition Management API
The RabbitMQ queueing service is used in the University API to handle asynchronous processing of payment requests for tuition fees. When a payment request is made, it is sent to a RabbitMQ queue and A separate microservice consumes messages from this queue and processes the payment request.

# Design
## Assumptions
- The RabbitMQ server is running and accessible to the University API.

- The University API and the payment processing microservice are both connected to the same RabbitMQ server.

- Messages in the payment_queue are processed in a first-in-first-out (FIFO) manner.

- Payment request is created with 'SendPaymentMessage' and then consumed and processed with 'ConsumePaymentMessage' methods


## Issues Encountered
- Message Processing Reliability: Ensuring that messages are processed and consumed reliably was a key challenge. This was addressed by implementing message acknowledgments in the processing microservice to confirm successful processing before removing messages from the queue.


## Video
Youtube:


<a href="https://www.youtube.com/watch?v=CcB4yd48P34"><img src="https://img.youtube.com/vi/CcB4yd48P34/0.jpg" alt="vid" border="0" width="355" height="200" /></a>


Drive:


<a href="https://drive.google.com/file/d/1IOwHLna532tHRMty1EXN8nF7pu_z-3I5/view?usp=sharing"><img src="https://i.ibb.co/82jQpBK/vid.png" alt="vid" border="0" width="355" height="200" /></a>


