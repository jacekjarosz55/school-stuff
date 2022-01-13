def val_sign(val: int) -> chr:
    if(val < 10): return str(val)
    else: return chr(ord('A') + val - 10)


def dec_change_base(dec: int, base:int) -> str:
    if base < 2 or base > 20:
        raise ValueError("Base should be between 2-20") 
    
    result:str = ""
    while(dec > 0):
        dec, mod = divmod(dec,base)
        result = val_sign(mod) + result
    return result

while True:
    try:
        base = int(input("Input a number:"))
        decimal = int(input("What base? (2-20):"))

        if 2 > system or system > 20:
            raise ValueError

    except ValueError:
        print("Incorrect Data!")
        continue
    else:
        break


print(dec_change_base(decimal,base))





