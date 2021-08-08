


def getDigits(num):
  count = 0

  while num > 0:
    count += 1
    num = num // 10

  return count

def get_digit(number, n):
    return number // 10**n % 10


def createArray(num, digits):

  digitArray = []
  
  while digits > 0:
    div = get_digit(num, digits-1)
    digitArray.append(div)
    digits = digits - 1

  return digitArray


def checkPalindrome(num):

  digitArray = createArray(num, getDigits(num))
  lenCounter = len(digitArray)-1

  if len(digitArray) == 1:
    return True;

  for i in range(len(digitArray)):

    if(digitArray[i] != digitArray[lenCounter]):
      return False

    lenCounter = lenCounter - 1

  return True

    

x = 595
assert(checkPalindrome(x) == True)
x = 5555
assert(checkPalindrome(x) == True)
x = 5955
assert(checkPalindrome(x) == False)
x = 5
assert(checkPalindrome(x) == True)
x = 7777
assert(checkPalindrome(x) == True)
x = 7474
assert(checkPalindrome(x) == False)
x = 78888888887
assert(checkPalindrome(x) == True)

print("All tests passed.")


