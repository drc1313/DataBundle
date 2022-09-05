

function applyInputToURL(urlData, filledTokens)
{        
    filledTokens.forEach(m => urlData=urlData.replace(m.id, m.value));
    $.ajax({
        method: "GET",
        "headers": {
            "Apca-Api-Key-Id": "PKL0I8H0F4ZF32BHPJWC",
            "Apca-Api-Secret-Key": "x5HNbyrUxzqhbdS426eoDeubsFDaotEUjkVhdPfR"
        },
        url: urlData
    });
      
}