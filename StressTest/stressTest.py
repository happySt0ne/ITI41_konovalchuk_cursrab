from utilities.functions import *

client_data: List[str] = read_file('./stressTest_client.txt')
server_data: List[str] = read_file('./stressTest_server.txt')

client_time: List[datetime] = convert_to_datetime(client_data)
server_time: List[datetime] = convert_to_datetime(server_data)

elapsed_time: List[float] = get_elapsed(client_time, server_time)

avg: float = sum(elapsed_time) / len(elapsed_time)

print(f'Среднее время получения данных сервером: {avg:.3f}с')

