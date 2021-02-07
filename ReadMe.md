# Atari-Register-Labeler
Replaces Common Memory Locations for Registers with Proper Labels found in the Atari Equates Assembly Source File.

This will open up an assembly language listing in text format. Untokenized. Seeks on locations between $D400 and $D4FF. 
Used by Antic, GTIA, Pokey, PIA, and common shadow registers. 
For Example $D000 becomes HPOSP0
            $D001         HPOSP1
            $D002         HPOSP2
            $D002         HPOSP3

Also works with decimal equivelents.Able to detect if its a read register, precded with a LDA, LDX, LDY, CPX or a write register with a STA, STX, STY.

I have been receiving copies of source code for the Atari 8-bit computer and been asked to make these programs run from cartridges or on the Atari 5200.
The issue with the 5200 is that the registers have been changed. GTIA is located at $C000 and Pokey is located at $E000, and no PIA.



