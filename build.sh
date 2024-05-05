msbuild -p:Configuration=Release
cd bin/Release/net48
declare -a copy=("CutsceneFix.dll")
for i in "${copy[@]}"
do
    cp "$i" ../../../
done
cd ../../../
rm -rf "/home/eva/.local/share/Steam/steamapps/common/VA Proxy Demo/BepInEx/plugins/CutsceneFix.dll"
cp CutsceneFix.dll "/home/eva/.local/share/Steam/steamapps/common/VA Proxy Demo/BepInEx/plugins"