Exercise to create a .net core API indented to be used by a companies website/mobile app. 
 
The API will need support for various features including:
* Process/forward requests for various financial calculations to specialized backend services.
* The ability to submit applications for various different items including: loans, grants, or contract work.
* The ability to submit finalized data for contracts based on the applications submitted.
    * After submission send command or trigger an event on a message bus to complete the next step of the contracting process (e.g. creating the contract pdf and coordinating with some third party document signing service).

**TODO**
* Ensure proper validation
* Add error responses if applicable
* Add auth (.net core identity)