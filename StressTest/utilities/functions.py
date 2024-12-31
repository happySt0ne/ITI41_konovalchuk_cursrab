from typing import List
from datetime import datetime

def convert_to_datetime(data: List[str]) -> List[datetime]:
    result: List[datetime] = []
    time_format: str = "%H:%M:%S.%f"

    result = [datetime.strptime(item, time_format) for item in data if item.strip()]

    return result

def read_file(path: str) -> List[str]:
    result: List[str] = []
        
    with open(path, 'r') as file:
        result = file.read().split()

    return result

def get_elapsed(
    client: List[datetime],
    server: List[datetime]
) -> List[float]:
    result: List[float] = []

    for i in range(len(client)):
        result.append((server[i] - client[i]).total_seconds())

    return result
