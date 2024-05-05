# VAProxy.CutsceneFix

A simple fix for a minor oversight in the cutscene manager's code.

# Issue explanation

The original code was always waiting, no matter if you were skipping the cutscene or not.

The only difference was it dividing the wait time by the timescale, which is 10 while skipping.

This means that if you were going to wait 5 seconds in the original cutscene, you'd still be forced to wait 500ms while "skipping" said cutscene.

This fixes that, simply removing the wait time altogether if you're skipping the cutscene.