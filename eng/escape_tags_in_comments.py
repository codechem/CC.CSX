import sys
f = open(sys.argv[1], "r")
ignored_tags = ['summary']
lines = f.readlines()
tags = []
ln = 1
escape_chars = { "\"":   "&quot;", "'":   "&apos;", "<":   "&lt;", ">":   "&gt;", "&":   "&amp;" }
for line in lines: 
    # tag must be on one line
    tag = None
    in_tag = False
    is_closing = False
    startidx = endidx = -1
    idx = 0
    changed_line = ''
    for char in line:
        changed_line+=char
        if char == '<':
            if in_tag:
                raise Exception("Tag in tag is not allowed")
            in_tag = True
            tag = ''
            startidx = idx
            is_closing = False
        elif char == '>':
            if in_tag:
                endidx = idx
                if tag not in ignored_tags:
                    tag_str = line[startidx:endidx+1]
                    escaped_tag_str = tag_str
                    for key, value in escape_chars.items():
                        escaped_tag_str = escaped_tag_str.replace(key, value)
                    changed_line = changed_line.replace(tag_str, escaped_tag_str)
                startidx = endidx = -1
                in_tag = False
                tag = None
        elif in_tag:
            if char == '/':
                is_closing = True
            elif tag is not None:
                tag+=char
            else:
                raise Exception("Tag is None")
        idx+=1
    ln+=1
    print(changed_line.replace('\n', ''))





