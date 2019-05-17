# SPOILER ALERT!

My son and I have been having fun working through this:

## Progress

Boot UMIX

* Implimented universal machine in 30-lines of F#
* Ran `sandmark.umz` successfully
* Decrypted and decompressed `codex.umz` to `umix.um` (key: `(\b.bb)(\v.vv)06FHPVboundvarHRAk`, pipe to file, remove header)
* Booted `umix.um` and accessed guest account (pub: `INTRO.LOG=200@999999|35e6f52e9bc951917c73af391e35e1d`)
* Checked mail, found house loan junk mail (pub: `INTRO.MUA=5@999999|b9666432feff66e528a17fb69ae8e9a`)
* Ran `a.out`, `cat core` (pub: `INTRO.OUT=5@999999|69ca684f8c787cfe06694cb26f74a95`)

Hack Accounts

* Examine hacking code: `cd code/`, `cat hack.bas` (repare trailing corruption)
* Transfer fixed: `/bin/umodem hack_fixed.bas STOP` (pub: INTRO.UMD=10@999999|7005f80d6cd9b7b837802f1e58b11b8)
* Compile: `/bin/qbasic hack_fixed.bas`
* Run against users in (`ls /home`, e.g. `hack_fixed.exe howie`)

## Passwords

| User | Password |
| --- | --- |
| `howie` | `xyzzy` |
| `guest` | `airplane` |
| `ohmega` | `bidirectional` |
