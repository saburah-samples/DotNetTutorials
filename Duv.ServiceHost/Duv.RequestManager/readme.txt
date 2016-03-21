@startuml
title State flow - process request
[*] --> InQueue : submit request
InQueue --> Canceled : cancel request
InQueue --> InProcess : start process
InProcess --> Completed : end process\n(successfully)
InProcess --> Failed : end process\n(with errors)
Canceled --> [*]
Completed --> [*]
Failed --> [*]
InQueue : request is in queue
Canceled : request is canceled
InProcess : request is in process
Completed : process has been completed successfully
Failed : process has been completed with errors
@enduml