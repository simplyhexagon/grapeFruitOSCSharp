# TO-DO List // Roadmap

## DONE
- Use *""* in file paths ✅
- Copy, move, delete commands (`cp`, `mv`, `rm`, `rm -r`, `rm -d`) ✅
- Make a custom `choice` function that can have either Y or N as the default action on Enter key ✅

### WIP
- Debug menu

### High priority
- Support for *REAL* FAT32 (hard drives) and ISO9660 (optical discs)
- Networking tweaks

### Low priority, future plans
- Possible large shell rework
	- Up and down arrow to browse command buffer
	- `^C` interrupt
	- Could potentially involve an arbitrary implementation of something like stdin-stdout
- Login system (store usernames and passwords on disk)
- Proper installer for the system
- Custom EFI bootloader