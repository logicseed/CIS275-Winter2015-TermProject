Discrete Structures I - CIS275 Winter 2015
======
>I was tasked with creating a simulation of John Conway's cellular automata - The Game of Life. 
>There were no specific technology requirements, so I chose to use this project as an opportunity 
>to experiment with GDI+ within WinForms. Additionally, I included the ability to modify the world 
>parameters using hotkeys, and a simple console application to perform a statistical analysis of the
>outcome of my algorithms.

### Details

__Student:__ Marc King

__Professor:__ Dr. Habib M. Ammari

__School:__ University of Michigan - Dearborn

__Timeline:__ 3 weeks

### Technologies

* .NET Framework 4.5
* GDI+

### Screenshots

#### Main Application

*Users are first presented with the splash screen that displays the title of the application.*

[![SplashScreen](Screenshots/SplashScreen_480.png?raw=true "SplashScreen")](Screenshots/SplashScreen.png?raw=true)

*After dismissing the splash screen, users are presented with a short description of the basic
premise of the Game of Life, and indicators for where information will be displayed and where the
help can be found.*

[![IntroductionPopup](Screenshots/IntroductionPopup_480.png?raw=true "IntroductionPopup")](Screenshots/IntroductionPopup.png?raw=true)

*The main screen consists of a faint grid representing the world, and the current settings.*

[![MainScreen](Screenshots/MainScreen_480.png?raw=true "MainScreen")](Screenshots/MainScreen.png?raw=true)

*Pulling up the help screen will show the hotkeys used to change the settings and control the simulation.*

[![HelpPopup](Screenshots/HelpPopup_480.png?raw=true "HelpPopup")](Screenshots/HelpPopup.png?raw=true)

*While the simulation is running, instances of life are indicated by colored squares.*

[![StandardRun](Screenshots/StandardRun_480.png?raw=true "StandardRun")](Screenshots/StandardRun.png?raw=true)

*The simulation can end in three possible states: Oscillation, Stabilization, or Extinction. When one
of those states is reached, the simulation will halt and a popup will display the result.*

[![OscillationPopup](Screenshots/OscillationPopup_480.png?raw=true "OscillationPopup")](Screenshots/OscillationPopup.png?raw=true)

[![StabilizationPopup](Screenshots/StabilizationPopup_480.png?raw=true "StabilizationPopup")](Screenshots/StabilizationPopup.png?raw=true)

*The size of the cells can be modified through hotkeys.*

[![SmallerCellSize](Screenshots/SmallerCellSize_480.png?raw=true "SmallerCellSize")](Screenshots/SmallerCellSize.png?raw=true)

*Smaller cell sizes can allow for more rows and columns in the world.*

[![LargerGrid](Screenshots/LargerGrid_480.png?raw=true "LargerGrid")](Screenshots/LargerGrid.png?raw=true)

[![LargerRun](Screenshots/LargerRun_480.png?raw=true "LargerRun")](Screenshots/LargerRun.png?raw=true)

*When exiting the user is prompted for confirmation.*

[![ExitPopup](Screenshots/ExitPopup_480.png?raw=true "ExitPopup")](Screenshots/ExitPopup.png?raw=true)

#### Statistics Collector Application

*The statistic collector application is a simple console application used to run my Game of Life algorithm
numerous times in order to perform a statistical analysis of its performance.*

[![StatCollectorInput](Screenshots/StatCollectorInput.png?raw=true "StatCollectorInput")](Screenshots/StatCollectorInput.png?raw=true)

[![StatCollectorProgress](Screenshots/StatCollectorProgress.png?raw=true "StatCollectorProgress")](Screenshots/StatCollectorProgress.png?raw=true)

[![StatCollectorResults](Screenshots/StatCollectorResults.png?raw=true "StatCollectorResults")](Screenshots/StatCollectorResults.png?raw=true)

