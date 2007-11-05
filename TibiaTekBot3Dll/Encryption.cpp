#include "stdafx.h"
#include "windows.h"
#include "Encryption.h"
#include "TibiaTekBot3Dll.h"

void tean(long *v, long *k, long N){      /* replaces TEA's code and decode */
	unsigned long y = v[0],
                  z = v[1],
                  DELTA = 0x9e3779b9,
                  limit,
                  sum;
    if (N > 0){ /* coding */
		limit = DELTA * N;
        sum = 0; 
        while (sum != limit){
			y   += (z << 4 ^ z >> 5) + z ^ sum + k[sum & 3];
            sum += DELTA;
            z   += (y << 4 ^ y >> 5) + y ^ sum + k[sum >> 11 & 3];
        }
	} else{	/* decoding */
		sum = DELTA * (-N);
        while (sum){
			z   -= (y << 4 ^ y >> 5) + y ^ sum + k[sum >> 11 & 3];
            sum -= DELTA;
            y   -= (z << 4 ^ z >> 5) + z ^ sum + k[sum & 3];
        }
    }
    v[0] = y;
    v[1] = z;
}

long EncipherTibia(void* p1, void* p2){
	char* data = (char*)p1;
	char* key = (char*)p2;
	unsigned char i1 = *(char*)p1;
	unsigned char i2 = *(((char*)p1)+1);
	unsigned int iCount = i1+(i2<<8);
	data += 2;
	for(unsigned int i=0; i < iCount/8; i++){
		tean((long*)data, (long*)key, 32);
		data += 8;
	}
	return iCount;
}

long DecipherTibia(void* p1, void* p2){
	char* data = (char*)p1;
	char* key = (char*)p2;
	unsigned char i1 = *(char*)p1;
	unsigned char i2 = *(((char*)p1)+1);
	unsigned int iCount = i1+(i2<<8);
	data += 2;
	for(unsigned int i=0; i < iCount/8; i++){
		tean((long*)data, (long*)key, -32);
		data +=8;
	}
	return iCount;
}

BYTE* getEncryptedCopy(BYTE *buf, int len){
	BYTE xGameKey[16];
	memcpy(xGameKey, (void*)TibiaTekBot3::XTEAKEY, 16);
	BYTE *data = new BYTE[len];
	memcpy(data, buf, len);
	int iPos = 0;
	while(iPos < len-1){
		EncipherTibia(data+iPos, xGameKey);
		int iLength = data[iPos] + (data[iPos]<<8);
		iPos += iLength+2;
	}
	return data;
}

BYTE* getDecryptedCopy(BYTE *buf, int len){
	BYTE xGameKey[16];
	memcpy(xGameKey, (void*)TibiaTekBot3::XTEAKEY, 16);
	BYTE *data = new BYTE[len];
	memcpy(data, buf, len);
	int iPos = 0;

	while(iPos < len-1){
		DecipherTibia(data+iPos, xGameKey);
		int iLength = data[iPos] + (data[iPos]<<8);
		iPos += iLength + 2;
	}
	return data;
}
