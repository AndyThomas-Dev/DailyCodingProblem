import binascii


# Covert str to byte array, apply map function and join as return string
def convertToBinary(incomingStr: str) -> str:
    return ''.join(map('{:08b}'.format, bytearray(incomingStr, encoding='utf-8')))


def flipBits(incomingStr: str) -> str:
    return ''.join([flipChar(x) for x in incomingStr])


def flipChar(inputChar) -> str:
    return '0' if inputChar == '1' else '1'


print(convertToBinary("Test!"))
print(flipBits(convertToBinary("Test!")))
