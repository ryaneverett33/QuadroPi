README
---
In the basic model QuadroPi model, there are three components of communication. These three components can send and recieve json files, referred to as a PUSH and a RETURN. 
The three components are: 
1. The Joystick Controller program, referred to as controller.
2. The Quadrocopter, referred to as host.
3. The QuadroCam app (optional), referred to as cam.

These three individual components can communicate via the JSON examples located in these folders. Each component should consistently main the standards derived in these files and never alter. Traditional variable types, such as an integer or a boolean, use their names as placeholders for values. Meaning, if the JSON example calls for an integer, then the application shall set an integer there.

 "machineid" : int 
	-equals- </br>
 "machineid" : 1234 </br>
These standards are built for modularity. More components can be added in as time progresses.
 
Variable Definitions
---
* machineid : "The unique identification number for each component. A completely random number that is used for login to validate communcation travel. The MachineID can be manually set for each component (TODO) by loading a preference file (JSON based)"
* password : "This string is system wide but only used during initial log in. When the joystick application loads the preference file, containing the password, or the user inputs a password, the password is uploaded to the host during the login process. If a password has not been set, the host will accept the new string and store it. This string will constantly be checked as the master password whenever one of the other components attempts a login. The password can be reset by initating the `reset_pwd()` function and PUSHing the MachineID as the initial argument followed by the new password. EX: `reset_pwd(1234,pi);`"
* type : "The type of PUSH/RETURN being created."
