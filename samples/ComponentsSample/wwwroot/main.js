import { dotnet } from './_framework/dotnet.js'

// CC.CSX.Browser brings its own JS glue (embedded module); just boot the runtime.
const { runMain } = await dotnet.create();
await runMain();
