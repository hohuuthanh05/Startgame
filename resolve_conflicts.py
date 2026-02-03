import re

file_path = r'd:\G2_FGU301\Startgame\Assets\Scenes\Game.unity'

with open(file_path, 'r', encoding='utf-8') as f:
    content = f.read()

# Pattern to match conflict markers and keep HEAD version
pattern = r'<<<<<<< HEAD\n(.*?)\n=======\n.*?\n>>>>>>> parent of 4be7de9 \(UPDATE\)\n?'
resolved = re.sub(pattern, r'\1\n', content, flags=re.DOTALL)

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(resolved)

print('Merge conflicts resolved - kept HEAD version')
