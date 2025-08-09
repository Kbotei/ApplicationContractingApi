Exercise to create a .net core API indented to be used by a companies mobile app. 
 
The mobile app will need support for various features including:
* Process/forward requests for various financial calculations to specialized backend services.
* The ability to submit applications, potentially based on the above calculations.
* The ability to submit finalized data for the contracting process.
    * After submission send command or trigger an event on a message bus to complete the next step of the contracting process (e.g. creating the contract pdf and coordinating with some third party document signing service).

**TODO**
* Ensure proper validation
* Add error responses if applicable
* Add auth (.net core identity)